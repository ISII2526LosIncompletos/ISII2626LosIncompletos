using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs.HerramientaDTOs;


namespace AppForSEII2526.UT.OfertasController_test
{
    public class GetHerramientasParaOferta_test : AppForSEII25264SqliteUT
    {
        private readonly ApplicationDbContext _context;

        public GetHerramientasParaOferta_test()
        {
            _context = CreateContext();

            var fabricantes = new List<Fabricante>
            {
                new Fabricante { Id = 1, Nombre = "Phillips" },
                new Fabricante { Id = 2, Nombre = "Wurt" }
            };

            var herramientas = new List<Herramienta>
            {
                new Herramienta { Id = 1, Nombre = "Destornillador", Material = "Acero", Precio = 12.50m, TiempoReparacion = 1, FabricanteId = 2, Fabricante = fabricantes[1] },
                new Herramienta { Id = 2, Nombre = "Llave Inglesa", Material = "Acero", Precio = 10.30m, TiempoReparacion = 2, FabricanteId = 1, Fabricante = fabricantes[0] }
            };

            _context.Fabricantes.AddRange(fabricantes);
            _context.Herramientas.AddRange(herramientas);
            _context.SaveChanges();
        }

        public static IEnumerable<object[]> TestCasesFor_GetHerramientasParaOferta_OK()
        {
            var dtoDestornillador = new HerramientasDTO { HerramientaID = 1, Nombre = "Destornillador", Material = "Acero", Fabricante = "Wurt", Precio = 12.50m, TiempoReparacion = 1 };
            var dtoLlave = new HerramientasDTO { HerramientaID = 2, Nombre = "Llave Inglesa", Material = "Acero", Fabricante = "Phillips", Precio = 10.30m, TiempoReparacion = 2 };

            var casoSinFiltros = new List<HerramientasDTO> { dtoDestornillador, dtoLlave };

            var casoPrecioBajo = new List<HerramientasDTO> { dtoLlave };

            var casoFabricante = new List<HerramientasDTO> { dtoLlave };

            return new List<object[]>
            {
                new object[] { null, null, casoSinFiltros },
                new object[] { 11.00m, null, casoPrecioBajo },
                new object[] { null, "Phillips", casoFabricante }
            };
        }

        [Theory]
        [MemberData(nameof(TestCasesFor_GetHerramientasParaOferta_OK))]
        [Trait("Database", "WithoutFixture")]
        [Trait("LevelTesting", "Unit Testing")]
        public async Task GetHerramientasParaOferta_OK(decimal? precio, string? fabricante, List<HerramientasDTO> ofertasEsperadas)
        {
            var logger = new Mock<ILogger<HerramientasController>>().Object;
            var controller = new HerramientasController(_context, logger);

            var result = await controller.GetOfertas(fabricante, precio);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var herramientasActuales = Assert.IsType<List<HerramientasDTO>>(okResult.Value);

            Assert.Equal(ofertasEsperadas.Count, herramientasActuales.Count);

            ofertasEsperadas = ofertasEsperadas.OrderBy(x => x.HerramientaID).ToList();
            herramientasActuales = herramientasActuales.OrderBy(x => x.HerramientaID).ToList();

            for (int i = 0; i < ofertasEsperadas.Count; i++)
            {
                Assert.Equal(ofertasEsperadas[i].HerramientaID, herramientasActuales[i].HerramientaID);
                Assert.Equal(ofertasEsperadas[i].Nombre, herramientasActuales[i].Nombre);
                Assert.Equal(ofertasEsperadas[i].Precio, herramientasActuales[i].Precio);
                Assert.Equal(ofertasEsperadas[i].Fabricante, herramientasActuales[i].Fabricante);
            }
        }
    }
}