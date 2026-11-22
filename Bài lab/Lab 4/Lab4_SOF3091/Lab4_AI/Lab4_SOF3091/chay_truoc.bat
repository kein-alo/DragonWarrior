@echo off
cd /d "%~dp0Lab4_SOF3091"
echo ===== TRUOC REFACTOR: LoginTests goc (con By) =====
dotnet test --filter "FullyQualifiedName~FirstTests.LoginTests" --logger "console;verbosity=normal"
echo.
echo ===== XONG. Hay chup man hinh nay, luu thanh screenshots\lab4_truoc_refactor.png =====
pause
