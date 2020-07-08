#!/bin/bash
mode=$1
basedir=$(dirname "$0")
serviceName=$(./readJson.py $mode)

dir=$(./readJson.py "linuxPath")

user=$(whoami)
echo "user $user"

str="
[Unit]
Description=Android Link - $serviceName
After=network.target
StartLimitIntervalSec=0

[Service]
Type=simple
User=$user
ExecStart=/usr/bin/node $dir/$1/index.js

[Install]
WantedBy=multi-user.target
"
echo $str

sudo mkdir -p $dir
sudo chmod 755 $dir
sudo rm -r $dir
sudo cp -r "$basedir"/../../../* $dir

echo "$str" | sudo tee /etc/systemd/system/android-link-"$serviceName".service
