<p align="center">
  <img src="https://img.icons8.com/?size=512&id=55494&format=png" width="20%" alt="REALTIME_CHATAPP-logo">
</p>
<p align="center">
    <h1 align="center">REALTIME CHATAPP</h1>
</p>
<p align="center">
    <em><code>❯ Built in order to Chatting</code></em>
</p>
<p align="center">
	<img src="https://img.shields.io/github/license/suaybdemir/Realtime_ChatApp?style=flat&logo=opensourceinitiative&logoColor=white&color=f1f1f1" alt="license">
	<img src="https://img.shields.io/github/last-commit/suaybdemir/Realtime_ChatApp?style=flat&logo=git&logoColor=white&color=f1f1f1" alt="last-commit">
	<img src="https://img.shields.io/github/languages/top/suaybdemir/Realtime_ChatApp?style=flat&color=f1f1f1" alt="repo-top-language">
	<img src="https://img.shields.io/github/languages/count/suaybdemir/Realtime_ChatApp?style=flat&color=f1f1f1" alt="repo-language-count">
</p>
<p align="center">
		<em>Built with the .Net Core and them technologies:</em>
</p>
<p align="center">
	<img src="https://img.shields.io/badge/EditorConfig-FEFEFE.svg?style=flat&logo=EditorConfig&logoColor=black" alt="EditorConfig">
	<img src="https://img.shields.io/badge/HTML5-E34F26.svg?style=flat&logo=HTML5&logoColor=white" alt="HTML5">
	<img src="https://img.shields.io/badge/JSON-000000.svg?style=flat&logo=JSON&logoColor=white" alt="JSON">
</p>

<br>

#####  Table of Contents

- [ Overview](#-overview)
- [ Features](#-features)
- [ Repository Structure](#-repository-structure)
- [ Modules](#-modules)
- [ Getting Started](#-getting-started)
    - [ Prerequisites](#-prerequisites)
    - [ Installation](#-installation)
    - [ Usage](#-usage)
    - [ Tests](#-tests)
- [ Project Roadmap](#-project-roadmap)
- [ Contributing](#-contributing)
- [ License](#-license)
- [ Acknowledgments](#-acknowledgments)

---

##  Overview

<code>❯ Initi a chat with this app!</code>

---

##  Features

<code>❯ Peer to peer chatting</code>

---

##  Repository Structure

```sh
└── Realtime_ChatApp/
    ├── ChatAppServer
    │   ├── ChatAppServer.Frontend
    │   │   ├── Authentication.html
    │   │   ├── Index.html
    │   │   └── _Layout.html
    │   ├── ChatAppServer.WebAPI
    │   │   ├── ChatAppServer.WebAPI.csproj
    │   │   ├── ChatAppServer.WebAPI.csproj.user
    │   │   ├── ChatAppServer.WebAPI.http
    │   │   ├── ChatAppServer.WebAPI.sln
    │   │   ├── Controllers
    │   │   │   ├── AuthenticationController.cs
    │   │   │   └── ChatsController.cs
    │   │   ├── Data
    │   │   │   └── ApplicationContext.cs
    │   │   ├── Dtos
    │   │   │   ├── RegisterDto.cs
    │   │   │   └── SendMessageDto.cs
    │   │   ├── Hubs
    │   │   │   └── ChatHub.cs
    │   │   ├── Models
    │   │   │   ├── ApplicationUser.cs
    │   │   │   ├── Chat.cs
    │   │   │   ├── ErrorResponse.cs
    │   │   │   └── UserConnection.cs
    │   │   ├── Program.cs
    │   │   ├── Properties
    │   │   │   └── launchSettings.json
    │   │   ├── appsettings.Development.json
    │   │   ├── appsettings.json
    │   │   ├── bin
    │   │   │   └── Debug
    │   │   │       └── net8.0
    │   └── ChatAppServer.sln
    └── README.md
```

---

##  Modules


<details closed><summary>ChatAppServer.ChatAppServer.WebAPI</summary>

| File | Summary |
| --- | --- |
| [appsettings.json](https://github.com/suaybdemir/Realtime_ChatApp/blob/main/ChatAppServer/ChatAppServer.WebAPI/appsettings.json) | <code>❯ REPLACE-ME</code> |
| [appsettings.Development.json](https://github.com/suaybdemir/Realtime_ChatApp/blob/main/ChatAppServer/ChatAppServer.WebAPI/appsettings.Development.json) | <code>❯ Development.json</code> |
| [Program.cs](https://github.com/suaybdemir/Realtime_ChatApp/blob/main/ChatAppServer/ChatAppServer.WebAPI/Program.cs) | <code>❯ MiddleWare</code> |

</details>

<details closed><summary>ChatAppServer.ChatAppServer.WebAPI.Dtos</summary>

| File | Summary |
| --- | --- |
| [RegisterDto.cs](https://github.com/suaybdemir/Realtime_ChatApp/blob/main/ChatAppServer/ChatAppServer.WebAPI/Dtos/RegisterDto.cs) | <code>❯ RegisterDto</code> |
| [SendMessageDto.cs](https://github.com/suaybdemir/Realtime_ChatApp/blob/main/ChatAppServer/ChatAppServer.WebAPI/Dtos/SendMessageDto.cs) | <code>❯ SendMessageDto</code> |

</details>


<details closed><summary>ChatAppServer.ChatAppServer.WebAPI.Properties</summary>

| File | Summary |
| --- | --- |
| [launchSettings.json](https://github.com/suaybdemir/Realtime_ChatApp/blob/main/ChatAppServer/ChatAppServer.WebAPI/Properties/launchSettings.json) | <code>❯ launchSettings.json</code> |

</details>


<details closed><summary>ChatAppServer.ChatAppServer.WebAPI.Data</summary>

| File | Summary |
| --- | --- |
| [ApplicationContext.cs](https://github.com/suaybdemir/Realtime_ChatApp/blob/main/ChatAppServer/ChatAppServer.WebAPI/Data/ApplicationContext.cs) | <code>❯ ApplicationContext</code> |

</details>

<details closed><summary>ChatAppServer.ChatAppServer.WebAPI.Hubs</summary>

| File | Summary |
| --- | --- |
| [ChatHub.cs](https://github.com/suaybdemir/Realtime_ChatApp/blob/main/ChatAppServer/ChatAppServer.WebAPI/Hubs/ChatHub.cs) | <code>❯ ChatHub</code> |

</details>

<details closed><summary>ChatAppServer.ChatAppServer.WebAPI.Models</summary>

| File | Summary |
| --- | --- |
| [Chat.cs](https://github.com/suaybdemir/Realtime_ChatApp/blob/main/ChatAppServer/ChatAppServer.WebAPI/Models/Chat.cs) | <code>❯ Chat</code> |
| [ApplicationUser.cs](https://github.com/suaybdemir/Realtime_ChatApp/blob/main/ChatAppServer/ChatAppServer.WebAPI/Models/ApplicationUser.cs) | <code>❯ ApplicationUser</code> |
| [UserConnection.cs](https://github.com/suaybdemir/Realtime_ChatApp/blob/main/ChatAppServer/ChatAppServer.WebAPI/Models/UserConnection.cs) | <code>❯ UserConnection</code> |
| [ErrorResponse.cs](https://github.com/suaybdemir/Realtime_ChatApp/blob/main/ChatAppServer/ChatAppServer.WebAPI/Models/ErrorResponse.cs) | <code>❯ ErrorResponse</code> |

</details>

<details closed><summary>ChatAppServer.ChatAppServer.WebAPI.Controllers</summary>

| File | Summary |
| --- | --- |
| [AuthenticationController.cs](https://github.com/suaybdemir/Realtime_ChatApp/blob/main/ChatAppServer/ChatAppServer.WebAPI/Controllers/AuthenticationController.cs) | <code>❯ AuthenticationController</code> |
| [ChatsController.cs](https://github.com/suaybdemir/Realtime_ChatApp/blob/main/ChatAppServer/ChatAppServer.WebAPI/Controllers/ChatsController.cs) | <code>❯ ChatsController</code> |

</details>

<details closed><summary>ChatAppServer.ChatAppServer.Frontend</summary>

| File | Summary |
| --- | --- |
| [Authentication.html](https://github.com/suaybdemir/Realtime_ChatApp/blob/main/ChatAppServer/ChatAppServer.Frontend/Authentication.html) | <code>❯ Authentication.html</code> |
| [_Layout.html](https://github.com/suaybdemir/Realtime_ChatApp/blob/main/ChatAppServer/ChatAppServer.Frontend/_Layout.html) | <code>❯ _Layout.html</code> |
| [Index.html](https://github.com/suaybdemir/Realtime_ChatApp/blob/main/ChatAppServer/ChatAppServer.Frontend/Index.html) | <code>❯ Index.html</code> |

</details>

---

##  Getting Started

###  Prerequisites

**.Net Core**: `8.0`

###  Installation

Build the project from source:

1. Clone the Realtime_ChatApp repository:
```sh
❯ git clone https://github.com/suaybdemir/Realtime_ChatApp
```

2. Navigate to the project directory:
```sh
❯ cd Realtime_ChatApp
```

3. Install the required dependencies:
```sh
❯ dotnet build
```

###  Usage

To run the project, execute the following command:

```sh
❯ dotnet run
```

###  Tests

Execute the test suite using the following command:

```sh
❯ dotnet test
```

##  Contributing

Contributions are welcome! Here are several ways you can contribute:

- **[Report Issues](https://github.com/suaybdemir/Realtime_ChatApp/issues)**: Submit bugs found or log feature requests for the `Realtime_ChatApp` project.
- **[Submit Pull Requests](https://github.com/suaybdemir/Realtime_ChatApp/blob/main/CONTRIBUTING.md)**: Review open PRs, and submit your own PRs.
- **[Join the Discussions](https://github.com/suaybdemir/Realtime_ChatApp/discussions)**: Share your insights, provide feedback, or ask questions.

<details closed>
<summary>Contributing Guidelines</summary>

1. **Fork the Repository**: Start by forking the project repository to your github account.
2. **Clone Locally**: Clone the forked repository to your local machine using a git client.
   ```sh
   git clone https://github.com/suaybdemir/Realtime_ChatApp
   ```
3. **Create a New Branch**: Always work on a new branch, giving it a descriptive name.
   ```sh
   git checkout -b new-feature-x
   ```
4. **Make Your Changes**: Develop and test your changes locally.
5. **Commit Your Changes**: Commit with a clear message describing your updates.
   ```sh
   git commit -m 'Implemented new feature x.'
   ```
6. **Push to github**: Push the changes to your forked repository.
   ```sh
   git push origin new-feature-x
   ```
7. **Submit a Pull Request**: Create a PR against the original project repository. Clearly describe the changes and their motivations.
8. **Review**: Once your PR is reviewed and approved, it will be merged into the main branch. Congratulations on your contribution!
</details>

<details closed>
<summary>Contributor Graph</summary>
<br>
<p align="left">
   <a href="https://github.com{/suaybdemir/Realtime_ChatApp/}graphs/contributors">
      <img src="https://contrib.rocks/image?repo=suaybdemir/Realtime_ChatApp">
   </a>
</p>
</details>

---

---

##  Acknowledgments

- List any resources, contributors, inspiration, etc. here.

---
