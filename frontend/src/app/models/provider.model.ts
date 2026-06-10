export interface Provider { id: string; nit: string; name: string; websiteUrl: string; email: string; country: string; createdAt: string; serviceCount: number; }
export interface ProviderDetail extends Provider { services: ProviderServiceItem[]; }
export interface ProviderServiceItem { serviceId: string; name: string; hourlyRate: number; customHourlyRate?: number; effectiveHourlyRate: number; }
