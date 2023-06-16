namespace IPFox.Tests
{
    public class IPGeodataRepositoryTests
    {
        [Test]
        public async Task TestGetIPLocation_USAddr()
        {
            var segment = await IPGeodataRepository.GetIPLocationAsync("8.8.8.8");
            Assert.IsNotNull(segment);
            Assert.That(segment.Detail.Country, Is.EqualTo("美国"));
            Assert.That(segment.Detail.DataProvider, Is.EqualTo("Level3"));
        }
        [Test]
        public async Task TestGetIPLocation_DEAddr()
        {
            var segment = await IPGeodataRepository.GetIPLocationAsync("104.76.204.20");
            Assert.IsNotNull(segment);
            Assert.That(segment.Detail.Country, Is.EqualTo("德国"));
            Assert.That(segment.Detail.State, Is.EqualTo("法兰克福"));
            Assert.That(segment.Detail.City, Is.EqualTo("法兰克福"));
            Assert.That(segment.Detail.DataProvider, Is.EqualTo("阿卡迈"));
        }
    }
}