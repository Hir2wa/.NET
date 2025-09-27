# Simple Shopping Cart - Blazor Version

This is a simplified shopping cart application built with Blazor Server, based on a React TypeScript project. The code is designed to be easy to understand for C# beginners.

## Features

- **Product List**: View available products with images
- **Shopping Cart**: Add/remove items, update quantities
- **Simple Navigation**: Switch between products and cart views
- **Responsive Design**: Works on desktop and mobile devices

## Project Structure

### Models

- `Product.cs` - Represents a product with SKU, name, and price
- `CartItem.cs` - Represents an item in the shopping cart with quantity

### Services

- `ProductService.cs` - Provides product data (hardcoded for simplicity)
- `CartService.cs` - Manages shopping cart state and operations

### Components

- `Home.razor` - Welcome page with navigation to products
- `ProductList.razor` - Displays all available products
- `Cart.razor` - Shows cart items and allows quantity updates
- `Header.razor` - Navigation header with cart totals
- `MainLayout.razor` - Main application layout

## How It Works

1. **Product Display**: Products are loaded from `ProductService` and displayed in a grid
2. **Add to Cart**: Click "Add to Cart" button to add products to your cart
3. **Cart Management**:
   - View all items in your cart
   - Update quantities using dropdown selectors
   - Remove items using the trash button
   - See total items and price
4. **Order Placement**: Click "Place Order" to clear the cart and confirm your order

## Key C# Concepts Used

- **Dependency Injection**: Services are registered in `Program.cs` and injected into components
- **Data Binding**: Two-way binding for form inputs and cart quantities
- **Event Handling**: Button clicks and form changes trigger methods
- **LINQ**: Used for filtering and aggregating cart data
- **Properties**: Computed properties for totals and formatted prices

## Running the Application

1. Navigate to the project directory
2. Run `dotnet run`
3. Open your browser to the displayed URL (usually https://localhost:5001)
4. Start shopping!

## Learning Notes

This implementation is intentionally simple and uses basic C# patterns:

- No complex state management (unlike React's useReducer)
- Direct service injection instead of context providers
- Simple property binding instead of complex event systems
- Straightforward component structure

The goal is to make the code easy to read and understand for C# beginners while maintaining the same functionality as the original React application.
