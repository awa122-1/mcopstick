# 创造 ↔ 生存切换
execute if entity @s[gamemode=creative] run gamemode s @s
execute if entity @s[gamemode=creative] run say §e✔ 已切换为生存模式！

execute if entity @s[gamemode=survival] run gamemode c @s
execute if entity @s[gamemode=survival] run say §b✔ 已切换为创造模式！
