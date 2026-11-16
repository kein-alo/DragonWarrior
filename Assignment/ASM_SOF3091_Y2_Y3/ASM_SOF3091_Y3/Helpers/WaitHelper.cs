using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace ASM_SOF3091_Y3.Helpers
{
    /// <summary>
    /// Bộ chờ tường minh (WebDriverWait) dùng chung toàn dự án.
    /// Thay thế hoàn toàn lệnh dừng cứng: chờ ĐÚNG điều kiện, xong là đi tiếp ngay,
    /// quá timeout mới ném WebDriverTimeoutException kèm thông báo rõ ràng.
    /// </summary>
    public class WaitHelper
    {
        private readonly IWebDriver _driver;
        private readonly TimeSpan _timeout;

        public WaitHelper(IWebDriver driver, int timeoutSeconds = 10)
        {
            _driver = driver;
            _timeout = TimeSpan.FromSeconds(timeoutSeconds);
        }

        private WebDriverWait NewWait(TimeSpan? timeout = null, string? message = null)
        {
            var wait = new WebDriverWait(_driver, timeout ?? _timeout)
            {
                PollingInterval = TimeSpan.FromMilliseconds(250)
            };
            // Bỏ qua lỗi tạm thời khi DOM của React đang render lại
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException),
                                      typeof(StaleElementReferenceException));
            if (message != null) wait.Message = message;
            return wait;
        }

        /// <summary>Chờ phần tử xuất hiện VÀ hiển thị.</summary>
        public IWebElement Visible(By locator, TimeSpan? timeout = null) =>
            NewWait(timeout, $"Không thấy phần tử hiển thị: {locator}")
                .Until(d => { var e = d.FindElement(locator); return e.Displayed ? e : null; });

        /// <summary>Chờ phần tử hiển thị VÀ bấm được (Enabled).</summary>
        public IWebElement Clickable(By locator, TimeSpan? timeout = null) =>
            NewWait(timeout, $"Phần tử không bấm được: {locator}")
                .Until(d => { var e = d.FindElement(locator); return e.Displayed && e.Enabled ? e : null; });

        /// <summary>Chờ phần tử biến mất (hoặc không còn trong DOM).</summary>
        public bool Invisible(By locator, TimeSpan? timeout = null) =>
            NewWait(timeout, $"Phần tử vẫn còn hiển thị: {locator}")
                .Until(d =>
                {
                    try { return !d.FindElement(locator).Displayed; }
                    catch (NoSuchElementException) { return true; }
                    catch (StaleElementReferenceException) { return true; }
                });

        /// <summary>Chờ URL chứa chuỗi (dùng cho bước chuyển trang của SPA).</summary>
        public bool UrlContains(string fragment, TimeSpan? timeout = null) =>
            NewWait(timeout, $"URL không chứa '{fragment}'")
                .Until(d => d.Url.Contains(fragment));

        /// <summary>Chờ phần tử có text khác rỗng rồi trả về text (thông báo hiện trễ).</summary>
        public string TextNotEmpty(By locator, TimeSpan? timeout = null) =>
            NewWait(timeout, $"Phần tử không có nội dung: {locator}")
                .Until(d => { var t = d.FindElement(locator).Text; return string.IsNullOrWhiteSpace(t) ? null : t; })!;

        /// <summary>Chờ phần tử có đúng nội dung expected (vd badge đổi từ 1 sang 2).</summary>
        public bool TextIs(By locator, string expected, TimeSpan? timeout = null) =>
            NewWait(timeout, $"Text của {locator} không trở thành '{expected}'")
                .Until(d => d.FindElement(locator).Text == expected);

        /// <summary>Chờ số lượng phần tử đúng bằng expected.</summary>
        public IReadOnlyCollection<IWebElement> CountIs(By locator, int expected, TimeSpan? timeout = null) =>
            NewWait(timeout, $"Số phần tử {locator} không đạt {expected}")
                .Until(d => { var l = d.FindElements(locator); return l.Count == expected ? l : null; })!;
    }
}