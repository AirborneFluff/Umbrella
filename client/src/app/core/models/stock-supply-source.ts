import { PriceBreak } from './price-break';

export interface StockSupplySource {
  supplierSKU: string,
  stockUnits: string,
  packSize: number,
  minimumOrderQuantity: number,
  prices: PriceBreak[]
}
