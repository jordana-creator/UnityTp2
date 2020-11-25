import maya

cmds.select("AS350_Main_rotor","AS350_Tail_rotor")
cmds.rotate(0,0,360,"AS350_Main_rotor")
cmds.rotate(360,0,0,"AS350_Tail_rotor")

maya.cmds.setKeyframe("AS350_Main_rotor",time=0, attribute="rotateZ", value=0)
maya.cmds.setKeyframe("AS350_Main_rotor",time=10, attribute="rotateZ", value=360)

maya.cmds.setKeyframe("AS350_Tail_rotor",time=0, attribute="rotateX", value=0)
maya.cmds.setKeyframe("AS350_Tail_rotor",time=10, attribute="rotateX", value=360)