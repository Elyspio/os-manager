#!/bin/bash
mode=$1
basedir=$(dirname "$0")

dir=$("$basedir"/readJson.py "linuxPath")

user="app"
echo "user $user"
echo "dir $dir"

str="
[Unit]
Description=Android Link - $mode
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

sudo rm -rf $dir
sudo mkdir -p $dir
sudo chmod -R 777 $dir
sudo cp -r "$basedir"/../../* $dir

echo "$str" | sudo tee /etc/systemd/system/android-link-"$mode".service

echo "Starting service android-link-$mode.service"
sudo systemctl start android-link-"$mode".service
sudo systemctl status android-link-"$mode".service
