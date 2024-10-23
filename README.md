中文 - [English](./README_en.md)

# 请遵守开源协议

# 请遵守开源协议

# 请遵守开源协议

# 功能

### 被动技

- 移除视角转动限制
- 修复中文玩家名显示`□`
- 移除玩家名字长度限制（HUD与聊天）
- 移除只能发送小写限制
- 移除敏感词限制（房主需要）

### 主动技

- 按I疯狂转头
- 按住O张嘴
- ↑↓←→控制头的移动
- WASD控制身体移动
- 按住鼠标右键转动控制身体转动
- 按`delete`键重置位置与旋转
- 按住`鼠标滚轮键`拖动鼠标可以让头前后移动
- 按`左Shift`与`左Ctrl`分别为上下移动

### 做不到

- 看别人的牌
- 修改手中的牌型
- 不死

# 安装

1. 下载[BepInEx](https://github.com/BepInEx/BepInEx/releases/download/v5.4.23.2/BepInEx_win_x64_5.4.23.2.zip)
2. 将BepInEx解压至游戏根目录（[官方安装教程](https://docs.bepinex.dev/articles/user_guide/installation/index.html)）
3. 从[Release](https://github.com/dogdie233/LiarsBarEnhance/releases)
   下载[最新Dll本体](https://github.com/dogdie233/LiarsBarEnhance/releases/download/1.0.0/com.github.dogdie233.LiarsBarEnhance.dll)
4. 将放置在插件文件夹（即`<游戏根目录>/BepInEx/plugins`）如何没有`plugins`目前请手动创建

# 自己构建插件

***Release 会发布构建完成的插件，非必要不建议自行构建***

1. 确保已经安装了[.NET SDK](https://dotnet.microsoft.com/zh-cn/download)（兼容netstandard2.1的SDK如6.0或以上）
2. `cmd` 或者 `powershell` 等终端输入 `git clone https://github.com/dogdie233/LiarsBarEnhance.git `克隆本仓库到本地(前提安装
   `Git`网上教程一堆)或点击绿绿`code`按钮点击`Download Zip`下载解压
3. 设置环境变量`LiarsBarManaged`为`<游戏根目录>/Liar's Bar_Data/Managed/`或者从`<游戏根目录>/Liar's Bar_Data/Managed/`
   将[列表](#所需复制文件)中的dll文件复制到`lib`文件夹
4. 在项目根目录执行`dotnet build -c Release`
5. 将Output目录下的`com.github.dogdie233.LiarsBarEnhance.dll`便是插件本体，按照[安装](#安装)继续进行下一步

## 所需复制文件

- `Assembly-CSharp.dll`
- `Mirror.dll`
- `UnityEngine.UI`
- `Unity.TextMeshPro.dll`
- `Unity.Localization.dll`

# 其他

欢迎欢愉的功能贡献（影响游戏平衡除外），可以提起功能请求等待有兴趣的开发者实现  
[BepinEx5安装教程【Unity游戏Mod/插件制作教程02 - BepInEx的安装和使用-哔哩哔哩】](https://www.bilibili.com/read/cv8997496/)
