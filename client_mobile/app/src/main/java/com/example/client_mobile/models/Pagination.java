package com.example.client_mobile.models;

import java.util.List;

public class Pagination<TEntity> {
    public int PageIndex = 10;
    public int PageSize = 0;
    public int Count = 0;
    public List<TEntity> Data = null;
}
