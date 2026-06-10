export interface PagedResult<T> { items: T[]; totalCount: number; page: number; pageSize: number; totalPages: number; }
export interface DashboardData { providersByCountry: CountryCount[]; servicesByCountry: CountryCount[]; totalProviders: number; totalServices: number; totalCountries: number; }
export interface CountryCount { country: string; count: number; }
