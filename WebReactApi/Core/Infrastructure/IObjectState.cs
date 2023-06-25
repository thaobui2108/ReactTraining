using System.ComponentModel.DataAnnotations.Schema;

namespace WebReactApi.Core.Infrastructure
{
    public interface IObjectState
    {
        [NotMapped]
        ObjectState ObjectState { get; set; }
    }
}
