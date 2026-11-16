#!/usr/bin/env bash
# Chạy Y3 ba lần liên tiếp, lưu log + .trx từng lần vào Reports/ (để chụp ảnh minh chứng)
set -u
cd "$(dirname "$0")"
mkdir -p Reports
pass=0
for i in 1 2 3; do
  echo "================ LẦN CHẠY $i/3 ================"
  dotnet test ASM_SOF3091_Y3/ASM_SOF3091_Y3.csproj \
    --logger "trx;LogFileName=Y3_run$i.trx" \
    --logger "console;verbosity=normal" \
    --results-directory ./Reports 2>&1 | tee "Reports/Y3_run$i.log"
  [ "${PIPESTATUS[0]}" -eq 0 ] && pass=$((pass+1))
done
echo "KẾT QUẢ: $pass/3 lần XANH"
