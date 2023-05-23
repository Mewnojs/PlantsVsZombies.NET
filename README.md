# PlantsVsZombies.net

[→English Version←](./README.en.md)

[中文Wiki](https://github.com/Mewnojs/PlantsVsZombies.NET/wiki)

#### 介绍
C#写就的植物大战僵尸手机版，现移植至PC端并作为开源项目提交

#### 架构
整个项目由**Lawn**、**Sexy**与**Sexy.TodLib**三个模块组成：
**Lawn**包含一些关于游戏机制的逻辑代码以及枚举值等；
**Sexy主库**内含各种资源的管理器与基本的UI组件，并负责实现多媒体支持、输入处理与文件解析；
Sexy.TodLib则负责各种渲染工作，包括动画、粒子、过渡曲线等。

#### 安装教程

1.  使用VS2019打开项目的sln文件;
2.	编译主程序;
3.	将资源文件包（涉及素材版权问题，请自行获取或求助交流群群友）解压至编译好的程序目录下，保证Contents文件夹与Lawn.exe在同一目录下；
4.	开始调试或运行。

#### 使用说明

1.  这是一个早期版本，有许多不稳定的地方，还请劳烦多多在issue中汇报问题
2.  存档文件目前存放在 %USERPROFILE%\AppData\Local\IsolatedStorage\任意文件夹\任意文件夹\任意文件夹\AppFiles\docs\userdata 位置，暂不兼容其他PvZ版本的存档
3.  游戏自带按键秘籍功能，如正常游玩请尽量不要触碰键盘

#### 参与贡献

1.  Fork 本仓库
2.  新建 Feat_xxx 分支
3.  提交代码
4.  新建 Pull Request


#### 联系方式

- 移植作者：2508大帝
- 项目交流群：884792079
- Discord Link：[discord.gg/uXz6g6Adnm](https://discord.gg/uXz6g6Adnm) 