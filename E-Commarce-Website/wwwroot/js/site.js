document.addEventListener("DOMContentLoaded", function () {
    // Mock data
    const data = {
        totalSales: 50000,
        totalOrders: 1200,
        totalUsers: 3000,
        totalRevenue: 75000
    };

    document.getElementById("totalSales").textContent = "$" + data.totalSales.toLocaleString();
    document.getElementById("totalOrders").textContent = data.totalOrders;
    document.getElementById("totalUsers").textContent = data.totalUsers;
    document.getElementById("totalRevenue").textContent = "$" + data.totalRevenue.toLocaleString();
});
