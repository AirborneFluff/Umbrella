import { StockSupplySource } from './stock-supply-source';

export interface StockItem {
  id: string,
  partCode: string,
  description: string,
  location: string | undefined,
  category: string | undefined,
  supplySources: StockSupplySource[]
}
