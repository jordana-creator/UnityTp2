import maya

cmds.group("helicoptere")

cmds.file("helicoptere", exportSelected = True,type = "FBX export")
