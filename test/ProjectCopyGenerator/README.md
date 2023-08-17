# one-key copy project to generate a new one

## usage

Set `appsettings.json`

```json

{
  "SourceDirectory": "/Path/to/your/project-copy-server", // any project you want to copy
  "TargetDirectory": "/Path/to/your/target-project-server", // must be empty forder, a new forder will be create when not exists 
  "NameMapping": { // any codes like "ProjectCopyServer" in files will be replace to "NewProjectServer"
    "From": "ProjectCopyServer",
    "To": "NewProjectServer"
  },
  "IgnorePatterns": [ // set ignore files here
    ".git/",
    ".idea/",
    "**/.idea/",
    "**/bin/",
    "**/obj/",
    "test/ProjectCopyGenerator/" // if you want incloud this `ProjectCopyGenerator` to new project, remove this line
    // add any files you want to ignore here
  ]
}
```

Restore project

```shell
> dotnet resore
```

build project

```shell
> dotnet build
```

Finally, run `Program.cs` 

you will find new codes in folder `/Path/to/your/target-project-server`

