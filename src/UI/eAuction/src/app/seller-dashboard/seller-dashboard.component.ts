import { Component, OnInit } from '@angular/core';
import { IProduct } from '../shared/models/product.model';
import { ProductService } from '../shared/services/product.service';

@Component({
  selector: 'app-seller-dashboard',
  templateUrl: './seller-dashboard.component.html',
  styleUrls: ['./seller-dashboard.component.scss']
})
export class SellerDashboardComponent implements OnInit {

  constructor(private productService:ProductService) { }
  
   productList: IProduct[]=[];
   productDetails: IProduct = {} as IProduct;
   displayDetails: boolean=false;
   selectProductFlag:boolean= false;

  ngOnInit(): void {
    this.getAllProducts();
  }

  getAllProducts() {
    this.productService.getAllProducts().subscribe((data: IProduct[]) => {
       this.productList = data;
    });
  }

  selectedProduct(event : any){
    console.log(event.target.value);
    if(event.target.value!="-1"){
      this.selectProductFlag = false;
      this.getBidDetailsForProductId(event.target.value);
    }
    else{
      this.selectProductFlag = true;
    }
      
  }

  getBidDetailsForProductId(productId:string){
    this.productService.getAllBidsForProductId(productId).subscribe((data:IProduct)=>{
       this.displayDetails=true;
       this.productDetails = data;
    });
  }

}
