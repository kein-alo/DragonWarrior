# Chứng minh toàn dự án KHÔNG còn Thread.Sleep (chụp ảnh kết quả "0 match")
$hits = Get-ChildItem -Recurse -Include *.cs | Where-Object { $_.FullName -notmatch '\\(bin|obj)\\' } |
        Select-String -Pattern 'Thread\.Sleep'
if ($hits) { $hits; Write-Host "CON Thread.Sleep!" -ForegroundColor Red; exit 1 }
else { Write-Host "0 ket qua Thread.Sleep - dat yeu cau" -ForegroundColor Green }
