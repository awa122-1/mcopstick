import { system, world, ModalFormData } from "@minecraft/server";

system.afterEvents.scriptEventReceive.subscribe(ev => {
  if (ev.id === "admin:open_menu") {
    const player = ev.sourceEntity;
    if (!player) return;

    // === 主菜单 ===
    const form = new ModalFormData()
      .title("§l§b管理员之棍菜单")
      .dropdown("选择操作", [
        "切换到创造模式",
        "击杀玩家"
      ])
      .submitButton("执行");

    form.show(player).then(response => {
      if (response.canceled) return;
      const choice = response.formValues[0];

      switch (choice) {
        // ======= 切换到创造模式 =======
        case 0:
          player.runCommandAsync("gamemode c @s");
          player.sendMessage("§a已切换为创造模式！");
          break;

        // ======= 击杀玩家 =======
        case 1:
          const players = world.getPlayers();
          if (players.length === 0) {
            player.sendMessage("§c当前无其他玩家可选择！");
            return;
          }

          // 构建玩家名字列表
          const names = players.map(p => p.name);

          const form2 = new ModalFormData()
            .title("§c选择要击杀的玩家")
            .dropdown("目标玩家", names)
            .submitButton("执行");

          form2.show(player).then(resp2 => {
            if (resp2.canceled) return;
            const index = resp2.formValues[0];
            const target = players[index];

            if (!target) {
              player.sendMessage("§c无效的目标！");
              return;
            }

            target.kill();
            world.sendMessage(`§c管理员 ${player.name} 击杀了 ${target.name}！`);
          });
          break;
      }
    });
  }
});
