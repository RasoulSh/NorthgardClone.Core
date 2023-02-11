namespace Northgard.Core.Infrastructure.Mapper
{
    public interface IReadOnlyMapper<TS,TT>
    {
        public TT MapToTarget(TS source);
    }
}