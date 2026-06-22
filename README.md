# Kanidm Windows client builder

The GitHub Actions workflow builds the Kanidm command-line client and publishes
two Windows artifacts:

- `kanidm-windows-x86_64.zip` for a portable installation.
- `kanidm-windows-x86_64.msi` for a system-wide installation.

The MSI installs `kanidm.exe` in `C:\Program Files\Kanidm` and adds that directory
to the system `PATH`. During installation it prompts for the Kanidm server URL,
defaulting to `https://idm.northlake.dev`, and creates the system-wide client
configuration at `C:\ProgramData\Kanidm\config`.

Existing configuration is never overwritten during installation or upgrades,
and the configuration remains available after uninstall. Run the installer as
an administrator, then open a new terminal before using `kanidm`. Uninstalling
the MSI removes the executable and its `PATH` entry.

For an unattended installation, set the public MSI property directly:

```powershell
msiexec /i kanidm-windows-x86_64-v1.10.3.msi `
  KANIDM_URI=https://idm.northlake.dev
```

Scheduled workflow runs skip Kanidm versions that already have a release. A
manual **Run workflow** always rebuilds the latest version and replaces its
existing ZIP, MSI, and checksum assets.
