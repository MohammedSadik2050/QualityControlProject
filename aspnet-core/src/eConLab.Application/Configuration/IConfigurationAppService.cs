using System.Threading.Tasks;
using eConLab.Configuration.Dto;

namespace eConLab.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
