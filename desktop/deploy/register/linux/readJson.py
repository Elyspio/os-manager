#!/usr/bin/python3
from os import path
import os
import json
import sys

scriptPath = path.dirname(os.path.realpath(__file__))

with open(path.join(scriptPath, "..", "const.json")) as f:
  data = json.load(f)[sys.argv[1]]
  print(data)