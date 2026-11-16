# Chạy Y3 ba lần liên tiếp, lưu log + .trx từng lần vào Reports/ (để chụp ảnh minh chứng)
Set-Location $PSScriptRoot
New-Item -ItemType Directory -Force -Path Reports | Out-Null
$pass = 0
1..3 | ForEach-Object {
  Write-Host "================ LAN CHAY $_/3 ================"
  dotnet test ASM_SOF3091_Y3/ASM_SOF3091_Y3.csproj `
    --logger "trx;LogFileName=Y3_run$_.trx" `
    --logger "console;verbosity=normal" `
    --results-directory ./Reports | Tee-Object -FilePath "Reports/Y3_run$_.log"
  if ($LASTEXITCODE -eq 0) { $pass++ }
}
Write-Host "KET QUA: $pass/3 lan XANH"
