#!/usr/bin/python3
import os
import json
import sys

scriptPath = os.path.dirname(os.path.realpath(__file__))

with open(os.path.join(scriptPath, "..", "const.json")) as f:
  data = json.load(f)[sys.argv[1]]
  print(data)
