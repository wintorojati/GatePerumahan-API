﻿namespace LeafByte.Parking.CrossCutting.Pagination;
public record PaginationRequest(int PageIndex = 0, int PageSize = 10);
