import ctypes, sys

def is_admin():
    try:
        return ctypes.windll.shell32.IsUserAnAdmin()
    except:
        return False

if not is_admin():
    print("Rerun in admin mode")
    exit(1)


import os
from shutil import copy
from pathlib import Path

SRC = os.path.abspath("pre-push ")
DST = os.path.abspath("../.git/hooks/pre-push")
if not os.path.exists(SRC):
    print(SRC)
    print("ERROR NOT EXIST")
    exit(1)
if os.path.exists(DST):
    os.remove(DST)
try:
    os.symlink(SRC, DST)
except Exception as e:
    print(e)
    print("ERROR")
    exit(1)

print("SYNC DONE")