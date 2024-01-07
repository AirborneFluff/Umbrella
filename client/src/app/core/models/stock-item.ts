import { StockSupplySource } from './stock-supply-source';

export interface StockItem {
  partCode: string,
  description: string,
  location: string,
  supplySources: StockSupplySource[]
}
