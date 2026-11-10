import java.time.Duration;

import org.junit.jupiter.api.AfterEach;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import org.openqa.selenium.By;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.chrome.ChromeDriver;
import org.openqa.selenium.support.ui.ExpectedConditions;
import org.openqa.selenium.support.ui.WebDriverWait;

import static org.junit.jupiter.api.Assertions.assertEquals;

public class ExplicitWaitTest {
    private WebDriver driver;

    // Dùng ví dụ /1: phần tử #finish được thêm vào DOM sau khi nhấn Start.
    private static final String URL =
            "https://the-internet.herokuapp.com/dynamic_loading/1";

    @BeforeEach
    void setUp() {
        driver = new ChromeDriver();
        driver.manage().window().maximize();
    }

    @AfterEach
    void tearDown() {
        if (driver != null) {
            driver.quit();
        }
    }

    // TEST 1: KHÔNG DÙNG WAIT – dự kiến FAIL với NoSuchElementException.
    @Test
    void testKhongSuDungWait() {
        driver.get(URL);

        driver.findElement(By.cssSelector("#start button")).click();

        // Cố ý tìm ngay lập tức, không chờ.
        // Vì #finish chưa được thêm vào DOM nên Selenium sẽ ném exception.
        WebElement finish = driver.findElement(By.id("finish"));

        String actual = finish.getText().trim();
        assertEquals("Hello World!", actual);
    }

    // TEST 2: CÓ EXPLICIT WAIT – dự kiến PASS.
    @Test
    void testCoSuDungWait() {
        driver.get(URL);

        driver.findElement(By.cssSelector("#start button")).click();

        WebDriverWait wait = new WebDriverWait(
                driver,
                Duration.ofSeconds(10)
        );

        WebElement finish = wait.until(
                ExpectedConditions.visibilityOfElementLocated(By.id("finish"))
        );

        String actual = finish.getText().trim();
        System.out.println("Nội dung nhận được: " + actual);

        assertEquals("Hello World!", actual);
    }
}
