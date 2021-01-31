# auto-uninstall-edge
Uninstalls Edge from ProgramFiles as uninstalling it from Programs and Features is disabled.

*Note that this will __NOT__ remove Edge from &lt;SysDrive&gt;:\Windows\SystemApps as this might cause system instability due to some (Windows) apps using only Edge, for example in Windows' Mail app!*

## Features
- Automatically detects Edge installation path ( using SystemRoot environment variable)
- Supports multiple versions (matching install directory dynamically)

## Dependencies
- .NET Framework 2.0

## Administrative privileges
are needed because we query the "SystemRoot" (returning Windows installation path) environment variable, as well as starting the Edge setup with elevated privileges.

## License
Licensed under MIT License, see LICENSE
