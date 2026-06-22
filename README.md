# Kanidm Windows client builder

The GitHub Actions workflow builds the Kanidm command-line client and publishes
two Windows artifacts:

- `kanidm-windows-x86_64.zip` for a portable installation.
- `kanidm-windows-x86_64.msi` for a system-wide installation.

The MSI installs `kanidm.exe` in `C:\Program Files\Kanidm` and adds that directory
to the system `PATH`. Run the installer as an administrator, then open a new
terminal before using `kanidm`. Uninstalling the MSI removes both the executable
and its `PATH` entry.
