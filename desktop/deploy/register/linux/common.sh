#!/bin/bash
mode=$1
basedir=$(dirname "$0")

dir=$("$basedir"/readJson.py "linuxPath")

user="app"
echo "user $user"
echo "dir $dir"

afterService=""

if [ "$mode" = "client" ]; then
  afterService="android-link-server.service"
fi

str="
[Unit]
Description=Android Link - $mode
After=network.target $afterService
StartLimitIntervalSec=0

[Service]
Type=simple
User=$user
Environment=\"NODE_ENV=production\"
ExecStart=/usr/local/bin/node $dir/$1/index.js

[Install]
WantedBy=multi-user.target
"
echo $str

sudo rm -rf $dir
sudo mkdir -p $dir
sudo chmod -R 777 $dir
sudo cp -r "$basedir"/../../* $dir

echo "$str" | sudo tee /etc/systemd/system/android-link-"$mode".service

sudo systemctl daemon-reload

echo "Starting service android-link-$mode.service"
sudo systemctl enable android-link-"$mode".service
sudo systemctl start android-link-"$mode".service
sudo systemctl status android-link-"$mode".service
