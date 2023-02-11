namespace Northgard.Core.Infrastructure.Mapper
{
    public interface IMapper<TS,TT>
    {
        public TT MapToTarget(TS source);
        public TS MapToSource(TT target);
    }
}