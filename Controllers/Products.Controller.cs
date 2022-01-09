using Microsoft.AspNetCore.Mvc;

public class ProductsController : Controller
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _productService.GetAllProductsAsync());
    }

    public async Task<IActionResult> Details(int id)
    {
        return View(await _productService.GetProductByIdAsync(id));
    }
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Product product)
    {
        try
        {
            if (ModelState.IsValid)
            {
                await _productService.CreateProductAsync(product);
                return RedirectToAction(nameof(Index));
            }
        }
        catch (Exception)
        {
            ModelState.AddModelError("", "Unable to save changes.");
        }
        return View(product);
    }

    public async Task<IActionResult> Edit(int id)
    {
        return View(await _productService.GetProductByIdAsync(id));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Product product)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var dbProduct = await _productService.GetProductByIdAsync(id);
                if (await TryUpdateModelAsync<Product>(dbProduct))
                {
                    await _productService.UpdateProductAsync(dbProduct);
                    return RedirectToAction(nameof(Index));
                } 
            }
        }
        catch (Exception)
        {
            ModelState.AddModelError("", "Unable to save changes.");
        }
        return View(product);
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var dbProduct = await _productService.GetProductByIdAsync(id);
            if (dbProduct != null)
            {
                await _productService.DeleteProductAsync(dbProduct); 
            }
        }
        catch (Exception)
        {
            ModelState.AddModelError("", "Unable to delete. ");
        }
    
        return RedirectToAction(nameof(Index));
    }
}