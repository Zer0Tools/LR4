using LR4;
using (var context = new StoreContext())
{

    var q1 = context.Sources
        .GroupJoin(
            context.Clients,
            source => source.Id,
            client => client.SourceId,
            (source, clients) => new { source, clients }
        )
        .SelectMany(
            sc => sc.clients.DefaultIfEmpty(),
            (sc, client) => new { sc.source, client }
        )
        .Where(sc => sc.client == null)
        .Select(sc => new
        {
            SourceName = sc.source.Name,
        }).ToList();

    var q1_r = q1
        .Select(sourceName => new
        {
            SourceName = sourceName,
            NumSources = q1.Count
        })
        .ToList();

    Console.WriteLine("Источник без клиентов\t\tОбщее число исчтоников без клиентов \t\t");
    foreach (var source in q1_r)
        Console.WriteLine($"{source}"); 

    var q2 = context.Sources
        .Join(
            context.Clients,
            source => source.Id,
            client => client.SourceId,
            (source, client) => new { source, client }
        )
        .GroupJoin(
            context.Sales,
            sc => sc.client.Id,
            sale => sale.ClientId,
            (sc, sales) => new { sc.source, sc.client, sales }
        )
        .SelectMany(
            scs => scs.sales.DefaultIfEmpty(),
            (scs, sale) => new { scs.source, sale }
        )
        .Where(scs => scs.sale == null)
        .Select(scs => scs.source.Name)
        .Distinct()
        .ToList();

    var q2_r = q2
        .Select(sourceName => new
        {
            SourceName = sourceName,
            NumSources = q2.Count
        })
        .ToList();

    Console.WriteLine("Источник, клиенты которых не делали заказы\t\tОбщее число таких исчтоников \t\t");
    foreach (var source in q2_r)
        Console.WriteLine($"{source}"); 

var q3_1 = context.Sources
        .Join(
            context.Clients,
            source => source.Id,
            client => client.SourceId,
            (source, client) => new { source, client }
        )
        .Join(
            context.Sales,
            sc => sc.client.Id,
            sale => sale.ClientId,
            (sc, sale) => new { sc.source, sc.client, sale }
        )
        .Join(
            context.Statuses,
            scs => scs.sale.StatusId,
            status => status.Id,
            (scs, status) => new { scs.source, scs.client, scs.sale, status }
        )
        .Where(scs => scs.status.Name == "rejected")
        .Select(scs => scs.source.Name)
        .Distinct()
        .ToList();

    var q3_r = q3_1
        .Select(sourceName => new
        {
            SourceName = sourceName,
            NumSources = q3_1.Count
        })
        .ToList();

    Console.WriteLine("Источник, клиенты которых отменяли заказы\t\tОбщее число таких исчтоников \t\t");
    foreach (var source in q3_r)
        Console.WriteLine($"{source}"); 


    var q4_1 = context.Sources
        .GroupJoin(
            context.Clients,
            source => source.Id,
            client => client.SourceId,
            (source, clients) => new { source, clients }
        )
        .SelectMany(
            sc => sc.clients.DefaultIfEmpty(),
            (sc, client) => new { sc.source, client }
        )
        .Where(sc => sc.client == null)
        .Select(sc => sc.source.Name);

    var q4_2 = context.Sources
        .Join(
            context.Clients,
            source => source.Id,
            client => client.SourceId,
            (source, client) => new { source, client }
        )
        .GroupJoin(
            context.Sales,
            sc => sc.client.Id,
            sale => sale.ClientId,
            (sc, sales) => new { sc.source, sc.client, sales }
        )
        .SelectMany(
            scs => scs.sales.DefaultIfEmpty(),
            (scs, sale) => new { scs.source, scs.client, sale }
        )
        .Where(scs => scs.sale == null)
        .Select(scs => scs.source.Name)
        .Distinct();

    var q4_3 = context.Sources
        .Join(
            context.Clients,
            source => source.Id,
            client => client.SourceId,
            (source, client) => new { source, client }
        )
        .Join(
            context.Sales,
            sc => sc.client.Id,
            sale => sale.ClientId,
            (sc, sale) => new { sc.source, sc.client, sale }
        )
        .Join(
            context.Statuses,
            scs => scs.sale.StatusId,
            status => status.Id,
            (scs, status) => new { scs.source, scs.client, scs.sale, status }
        )
        .Where(scs => scs.status.Name == "rejected")
        .Select(scs => scs.source.Name)
        .Distinct();

    var q4_r = q4_1
        .Union(q4_2)
        .Union(q4_3)
        .ToList();

    Console.WriteLine("объедененный запрос");
    Console.WriteLine("Источники");
    foreach (var source in q4_r)
        Console.WriteLine($"{source}"); 

    var q5_r = context.SaleHistories
        .Join(
            context.Statuses,
            saleHistory => saleHistory.StatusId,
            status => status.Id,
            (saleHistory, status) => new { saleHistory, status }
        )
        .GroupBy(s => true)
        .Select(g => new
        {
            TotalOrders = g.Count(),
            RejectedOrders = g.Count(x => x.status.Name == "rejected")
        })
        .FirstOrDefault();

    var percentOfRejected = q5_r != null && q5_r.TotalOrders > 0
        ? Math.Round((q5_r.RejectedOrders / (decimal)q5_r.TotalOrders) * 100.0m, 2)
        : 0;

        Console.WriteLine("Процент отменненых заказов");
        Console.WriteLine(percentOfRejected);
}

