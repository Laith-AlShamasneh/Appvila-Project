namespace Repository.Interface;

public interface IGetListAction<T>
{
    Task<IList<T>> GetList(T entity);
}

public interface IGetByIdAction<T>
{
    public Task<T> GetById(T entity);
}

public interface IAddAction<T>
{
    Task Add(T entity);
}

public interface IAddGetObjectAction<T>
{
    Task<T> AddGetObject(T entity);
}

public interface IAddGetListAction<T>
{
    Task<IList<T>> AddGetList(T entity);
}

public interface IUpdateAction<T>
{
    Task Update(T entity);
}

public interface IUpdateGetObjectAction<T>
{
    Task<T> UpdateGetObject(T entity);
}

public interface IUpdateGetListAction<T>
{
    Task<IList<T>> UpdateGetList(T entity);
}

public interface IDeleteAction<T>
{
    Task Delete(T entity);
}

public interface IDeleteGetListAction<T>
{
    Task<IList<T>> DeleteGetList(T entity);
}

public interface IActiveAction<T>
{
    Task Active(T entity);
}

public interface IActiveGetListAction<T>
{
    Task<IList<T>> ActiveGetList(T entity);
}

public interface IGetListPaginationAction<T, in TPagination, in TPaginationSortColumn, in TPaginationSearchValue, in TSortDirection>
{
    Task<(IList<T> Result, long TotalRecords)> GetListPagination(
        T entity,
        TPagination pagination,
        TPaginationSortColumn? sortColumn,
        TSortDirection sortDirection,
        TPaginationSearchValue? searchValue);
}
