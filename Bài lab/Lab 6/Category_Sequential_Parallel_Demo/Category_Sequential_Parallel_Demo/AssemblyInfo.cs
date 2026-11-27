using NUnit.Framework;

// Cho phép các TestFixture chạy song song, tối đa 4 luồng.
[assembly: Parallelizable(ParallelScope.Fixtures)]
[assembly: LevelOfParallelism(4)]
