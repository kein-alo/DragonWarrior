@echo off
cd /d "%~dp0Lab4_SOF3091"
echo ===== SAU REFACTOR: LoginTests dung Page Object =====
dotnet test --filter "FullyQualifiedName~FirstTests.Tests.LoginTests" --logger "console;verbosity=normal"
echo.
echo ===== XONG. Hay chup man hinh nay, luu thanh screenshots\lab4_sau_refactor.png =====
pause
