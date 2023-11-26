export interface Product extends BaseProduct {
  id: string;
}

export interface BaseProduct {
  categoryId: string; //or Category
  name: string;
  description: string;
  price: number;
  color: string | undefined;
}
