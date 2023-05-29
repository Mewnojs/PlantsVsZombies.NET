# PlantsVsZombies.net

[→English Version←](./README.en.md)

[中文Wiki](https://github.com/Mewnojs/PlantsVsZombies.NET/wiki)

#### 介绍
C#写就的植物大战僵尸手机版，现移植至PC端并作为开源项目提交

#### 项目结构
整个项目由**Lawn**、**Sexy**与**Sexy.TodLib**三个模块组成：
**Lawn**包含一些关于游戏机制的逻辑代码以及枚举值等；
**Sexy主库**内含各种资源的管理器与基本的UI组件，并负责实现多媒体支持、输入处理与文件解析；
**Sexy.TodLib**则负责各种渲染工作，包括动画、粒子、过渡曲线等。
**LawnModExtension**是一个新模块，为游戏提供使用IronPython3修改的支持。

#### 安装教程

1.  使用VS2022打开项目的sln文件;
2.	编译主程序;
3.	将资源文件包（请见[联系方式](####联系方式)）解压至编译好的程序目录下，保证Contents文件夹与Lawn.exe在同一目录下；
4.	开始调试或运行。

#### 注意事项

1.  这是一个早期版本，有许多不稳定的地方，还请劳烦多多在issue中汇报问题
2.  存档文件目前存放在 `程序目录\docs\userdata` 位置 (从 v0.13.1 开始)

#### 修改游戏
你可以通过两种方法修改，其中各有优劣：
1. Fork本仓库并直接修改源代码或资源文件，发布修改后的版本；
2. 制作IronPython3脚本修改游戏，这种方法不需要修改源代码，而且可以兼容不同版本，但修改的内容有限，且需要对Hook技术有一定的了解。
对于第二种方法，可以参考Wiki里的LawnMod API文档中的示例。脚本文件放在 `程序目录\mods` 下，打开游戏即可启用。

#### 联系方式

- 移植作者：2508大帝
- 项目交流群：884792079
- Discord Link：[discord.gg/uXz6g6Adnm](https://discord.gg/uXz6g6Adnm) 