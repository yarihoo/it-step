export interface IProductItem{
    id: number,
    name: string,
    description: string,
    image: string,
    price: number,
    salePrice: number,
    subcategory: string,
    isDelete: boolean
  } 
  
  export interface ISubcategoryItem{
    id: number,
    name: string,
    category: string,
    products: IProductItem[]
  }
  
  export interface ICategoryItem{
    id: number,
    name: string,
    subcategories: ISubcategoryItem[]
  }

  export interface IBagItem{
    id: number,
    product: IProductItem,
    count: number
  }

  export interface IUserItem{
    id: number,
    email: string,
    firstName: string,
    lastName: string,
}