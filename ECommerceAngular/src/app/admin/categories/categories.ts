import { Component, OnInit } from '@angular/core';
import { AdminService } from '../../services/admin.service';
import { Category } from '../../models/category';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

@Component({
  selector: 'app-categories',
  standalone: true,
  imports: [FormsModule, HttpClientModule],
  templateUrl: './categories.html',
  styleUrls: ['./categories.css']
})
export class CategoriesComponent implements OnInit {
  categories: Category[] = [];
  newCategory: Category = { id: 0, name: '' };
  editingCategory: Category | null = null;

  constructor(private adminService: AdminService) {}

  ngOnInit() {
    this.loadCategories();
  }

  loadCategories() {
    this.adminService.getCategories().subscribe(data => this.categories = data);
  }

  addCategory() {
    this.adminService.addCategory(this.newCategory).subscribe(() => {
      this.loadCategories();
      this.newCategory = { id: 0, name: '' };
    });
  }

  editCategory(category: Category) {
    this.editingCategory = { ...category };
  }

  updateCategory() {
    if (this.editingCategory) {
      this.adminService.updateCategory(this.editingCategory).subscribe(() => {
        this.loadCategories();
        this.editingCategory = null;
      });
    }
  }

  deleteCategory(id: number) {
    this.adminService.deleteCategory(id).subscribe(() => this.loadCategories());
  }
}