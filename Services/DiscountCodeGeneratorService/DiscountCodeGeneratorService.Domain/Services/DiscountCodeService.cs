using DiscountCodeGeneratorService.Domain.Entities;
using DiscountCodeGeneratorService.Domain.Exceptions;
using DiscountCodeGeneratorService.Domain.Interfaces;
using System.Collections.Concurrent;

namespace DiscountCodeGeneratorService.Domain.Services;

public class DiscountCodeService(IDiscountCodeRepository discountCodeRepository) : IDiscountCodeService
{
    private const string ALLOWED_DISCOUNT_CODE_CHARACTERS = "ABCDEFGHJKLMNPQRSTUVWXYZ1234567890";
    // Prevent infinite loops
    private const int MAX_CODE_GENERATION_ATTEMPTS_PER_CODE = 10;
    private const int MAX_CODE_GENERATION_ATTEMPTS_PER_BATCH = 10;

    private const ushort CODE_GENERATION_BATCH_SIZE = 200;
    public async Task<IEnumerable<DiscountCode>> GenerateDiscountCodesAsync(ushort count, byte codeLength, CancellationToken cancellationToken)
    {
        ValidateDiscountCodeParameters(count, codeLength);

        var batchId = Guid.NewGuid();
        var generatedCodes = new ConcurrentBag<DiscountCode>();
        var totalBatchAttempts = 0;

        var discountCodeBatchTasks = new List<Task>();
        var remainingDiscountCodesToGenerate = count;
        var batchSize = Math.Min(CODE_GENERATION_BATCH_SIZE, count);
        bool maxAttemptsReached = totalBatchAttempts >= (batchSize * MAX_CODE_GENERATION_ATTEMPTS_PER_BATCH);

        while (remainingDiscountCodesToGenerate > 0 && !maxAttemptsReached)
        {
            var currentBatchSize = Math.Min(batchSize, remainingDiscountCodesToGenerate);

            var task = Task.Run(async () =>
            {
                var codes = await GenerateUniqueDiscountCodesPerBatchAsync(generatedCodes, currentBatchSize, batchId, codeLength, cancellationToken);
                foreach (var code in codes)
                {
                    generatedCodes.Add(code);
                }
            }, cancellationToken);

            discountCodeBatchTasks.Add(task);
            remainingDiscountCodesToGenerate -= currentBatchSize;
            totalBatchAttempts++;
        }

        await Task.WhenAll(discountCodeBatchTasks);

        var result = generatedCodes.Take(count).ToList();

        return result;
    }

    private void ValidateDiscountCodeParameters(ushort count, byte codeLength)
    {
        if (count <= 0 || count > 2000)
        {
            throw new InvalidCountWhenGeneratingDiscountCodes(count.ToString());
        }

        if (codeLength < 6 || codeLength > 12)
        {
            throw new InvalidCodeLengthWhenGeneratingDiscountCodes(codeLength.ToString());
        }
    }

    private async Task<List<DiscountCode>> GenerateUniqueDiscountCodesPerBatchAsync(ConcurrentBag<DiscountCode> generatedCodes, int count, Guid batchId, byte codeLength, CancellationToken cancellationToken)
    {
        var codes = new List<DiscountCode>();
        var attempts = 0;
        bool maxAttemptsReached = attempts >= MAX_CODE_GENERATION_ATTEMPTS_PER_CODE * count;

        while (codes.Count < count && !maxAttemptsReached)
        {
            var codeString = GenerateRandomDiscountCode(codeLength);
            bool isDiscountCodeExists = generatedCodes.Any(dc => dc.Code == codeString);
            bool isDiscountCodeExistsInDatabase =  await discountCodeRepository.IsDiscountCodeExistsAsync(codeString, cancellationToken);

            if (!isDiscountCodeExistsInDatabase && !isDiscountCodeExists)
            {
                codes.Add(new DiscountCode(codeString, null));
            }

            attempts++;
        }

        return codes;
    }

    private string GenerateRandomDiscountCode(byte codeLength)
    {
        var random = new Random();
        var randomCodeChars = new char[codeLength];

        for (int i = 0; i < codeLength; i++)
        {
            int index = random.Next(ALLOWED_DISCOUNT_CODE_CHARACTERS.Length);
            randomCodeChars[i] = ALLOWED_DISCOUNT_CODE_CHARACTERS[index];
        }

        return new string(randomCodeChars);
    }

    public async Task<byte> UseCode(DiscountCode code, CancellationToken cancellationToken)
    {
        code.MarkAsUsed(DateTime.UtcNow);
        await discountCodeRepository.SaveAsync(cancellationToken);

        return (byte)1;
    }
}
