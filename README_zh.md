# AElf DApp-server脚手架

## 使用步骤

- 打开代码生成 **子工程** `test/ProjectCopyGenerator/ProjectCopyGenerator.sln`
- 修改子工程配置文件：`test/ProjectCopyGenerator/ProjectCopyGenerator/appsettings.json`
```json
{
  "SourceDirectory": "/path/to/your/project-copy-server", // 要复制的模板工程
  "TargetDirectory": "/path/to/your/new-dapp-server",   // 复制目标工程，需要是空目录，目录不存在会自动新建
  "NameMapping": {  // 模板工程中的所有字符串都会被替换
    "From": "ProjectCopyServer",
    "To": "NewDappServer"
  },
  "ExcludedExtensions": [".dll", ""], // 忽略改名的文件类型
  "IgnorePatterns": [ // 不会复制的文件
    ".git/",
    ".idea/",
    "**/.idea/",
    "**/bin/",
    "**/obj/",
    "**/Logs/",
    "**/appsettings.json",
    "test/ProjectCopyGenerator/",
    "README_zh.md"
  ],
  "addFiles" : {  // 添加的新文件，这些配置文件将会是空文件， 生成目标工程后需要从appsettings.example.json中复制一份
    "./src/{NameMappingTo}.HttpApi.Host/appsettings.json": "{}",
    "./src/{NameMappingTo}.Silo/appsettings.json": "{}",
    "./src/{NameMappingTo}.DbMigrator/appsettings.json": "{}",
    "./src/{NameMappingTo}.EntityEventHandler/appsettings.json": "{}",
    "./src/{NameMappingTo}.ContractEventhandler/appsettings.json": "{}",
    "./test/{NameMappingTo}.HttpApi.Client.ConsoleTestApp/appsettings.json": "{}"
  }
  
}
```

- 执行子工程`test/ProjectCopyGenerator.Program.Main` 即可生成目标工程 `/path/to/your/new-dapp-server`
- 给子工程`scripts/*`添加可执行权限，以可以正常编译`.proto`文件：
```shell
> chmod +x /path/to/your/new-dapp-server/scripts/*
```

- 编译子工程，此时应该能够编译成功
```shell
> cd /path/to/your/new-dapp-server
> dotnet build
```

## 说明
- scripts/* 如果不添加执行权限，编译会报权限错误
- 子工程也可以复制到其他模板工程，用来生成其他工程，或者用于工程改名