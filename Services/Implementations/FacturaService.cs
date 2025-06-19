using GestionLogisticaBackend.DTOs.Cliente;
using GestionLogisticaBackend.DTOs.Envio;
using GestionLogisticaBackend.DTOs.Factura;
using GestionLogisticaBackend.DTOs.Ubicacion;
using GestionLogisticaBackend.Enums;
using GestionLogisticaBackend.Services.Interfaces;
using LogisticaBackend.Data;
using LogisticaBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionLogisticaBackend.Services.Implementations
{
    public class FacturaService : IFacturaService
    {
        private readonly LogisticaContext _context;

        public FacturaService(LogisticaContext context)
        {
            _context = context;
        }

        public async Task<List<FacturaDto>> GetFacturasAsync()
        {
            return await _context.Facturas
                .Select(f => new FacturaDto
                {
                    IdFactura = f.IdFactura,
                    NumeroFactura = f.NumeroFactura,
                    Envio = new EnvioDto
                    {
                        IdEnvio = f.IdEnvio,
                        Origen = new UbicacionDto
                        {
                            IdUbicacion = f.Envio.Origen.IdUbicacion,
                            Direccion = f.Envio.Origen.Direccion,
                            Localidad = new LocalidadDto
                            {
                                IdLocalidad = f.Envio.Origen.Localidad.IdLocalidad,
                                Nombre = f.Envio.Origen.Localidad.Nombre,
                                Provincia = new ProvinciaDto
                                {
                                    IdProvincia = f.Envio.Origen.Localidad.Provincia.IdProvincia,
                                    Nombre = f.Envio.Origen.Localidad.Provincia.Nombre,
                                    Pais = new PaisDto
                                    {
                                        IdPais = f.Envio.Origen.Localidad.Provincia.Pais.IdPais,
                                        Nombre = f.Envio.Origen.Localidad.Provincia.Pais.Nombre
                                    }
                                }
                            }
                        },
                        Destino = new UbicacionDto
                        {
                            IdUbicacion = f.Envio.Destino.IdUbicacion,
                            Direccion = f.Envio.Destino.Direccion,
                            Localidad = new LocalidadDto
                            {
                                IdLocalidad = f.Envio.Destino.Localidad.IdLocalidad,
                                Nombre = f.Envio.Destino.Localidad.Nombre,
                                Provincia = new ProvinciaDto
                                {
                                    IdProvincia = f.Envio.Destino.Localidad.Provincia.IdProvincia,
                                    Nombre = f.Envio.Destino.Localidad.Provincia.Nombre,
                                    Pais = new PaisDto
                                    {
                                        IdPais = f.Envio.Destino.Localidad.Provincia.Pais.IdPais,
                                        Nombre = f.Envio.Destino.Localidad.Provincia.Pais.Nombre
                                    }
                                }
                            }
                        },
                        FechaEntregaReal = f.Envio.FechaEntregaReal,
                    },
                    Cliente = new ClienteDto
                    {
                        IdCliente = f.Cliente.IdCliente,
                        Nombre = f.Cliente.Nombre,
                        Telefono = f.Cliente.Telefono,
                        Email = f.Cliente.Email
                    },
                    FechaEmision = f.FechaEmision,
                    Estado = f.Estado,
                    Subtotal = f.Subtotal,
                    Iva = f.Iva,
                    Total = f.Total
                }).ToListAsync();
        }

        public async Task<FacturaDto?> GetFacturaByIdAsync(int id)
        {
            var factura = await _context.Facturas.FirstOrDefaultAsync(f => f.IdFactura == id);

            if (factura == null) return null;

            return new FacturaDto
            {
                IdFactura = factura.IdFactura,
                NumeroFactura = factura.NumeroFactura,
                Envio = new EnvioDto
                {
                    IdEnvio = factura.IdEnvio,
                    Origen = new UbicacionDto
                    {
                        IdUbicacion = factura.Envio.Origen.IdUbicacion,
                        Direccion = factura.Envio.Origen.Direccion,
                        Localidad = new LocalidadDto
                        {
                            IdLocalidad = factura.Envio.Origen.Localidad.IdLocalidad,
                            Nombre = factura.Envio.Origen.Localidad.Nombre,
                            Provincia = new ProvinciaDto
                            {
                                IdProvincia = factura.Envio.Origen.Localidad.Provincia.IdProvincia,
                                Nombre = factura.Envio.Origen.Localidad.Provincia.Nombre,
                                Pais = new PaisDto
                                {
                                    IdPais = factura.Envio.Origen.Localidad.Provincia.Pais.IdPais,
                                    Nombre = factura.Envio.Origen.Localidad.Provincia.Pais.Nombre
                                }
                            }
                        }
                    },
                    Destino = new UbicacionDto
                    {
                        IdUbicacion = factura.Envio.Destino.IdUbicacion,
                        Direccion = factura.Envio.Destino.Direccion,
                        Localidad = new LocalidadDto
                        {
                            IdLocalidad = factura.Envio.Destino.Localidad.IdLocalidad,
                            Nombre = factura.Envio.Destino.Localidad.Nombre,
                            Provincia = new ProvinciaDto
                            {
                                IdProvincia = factura.Envio.Destino.Localidad.Provincia.IdProvincia,
                                Nombre = factura.Envio.Destino.Localidad.Provincia.Nombre,
                                Pais = new PaisDto
                                {
                                    IdPais = factura.Envio.Destino.Localidad.Provincia.Pais.IdPais,
                                    Nombre = factura.Envio.Destino.Localidad.Provincia.Pais.Nombre
                                }
                            }
                        }
                    },
                    FechaEntregaReal = factura.Envio.FechaEntregaReal,
                },
                Cliente = new ClienteDto
                {
                    IdCliente = factura.Cliente.IdCliente,
                    Nombre = factura.Cliente.Nombre,
                    Telefono = factura.Cliente.Telefono,
                    Email = factura.Cliente.Email
                },
                FechaEmision = factura.FechaEmision,
                Estado = factura.Estado,
                Subtotal = factura.Subtotal,
                Iva = factura.Iva,
                Total = factura.Total
            };
        }

        public async Task<FacturaDto> CreateFacturaAsync(CreateFacturaDto facturaDto)
        {
            if (facturaDto == null)
            {
                throw new ArgumentNullException(nameof(facturaDto), "La factura no puede ser nula.");
            }

            var factura = new Factura
            {
                IdEnvio = facturaDto.IdEnvio,
                IdCliente = facturaDto.IdCliente,
                NumeroFactura = facturaDto.NumeroFactura,
                FechaEmision = facturaDto.FechaEmision,
                FechaVencimiento = facturaDto.FechaVencimiento,
                Subtotal = facturaDto.Subtotal,
                Iva = facturaDto.Iva,
                Total = facturaDto.Total,
                Estado = facturaDto.Estado,
            };

            _context.Facturas.Add(factura);
            await _context.SaveChangesAsync();

            return new FacturaDto
            {
                IdFactura = factura.IdFactura,
                NumeroFactura = factura.NumeroFactura,
                Envio = new EnvioDto
                {
                    IdEnvio = factura.IdEnvio,
                    Origen = new UbicacionDto
                    {
                        IdUbicacion = factura.Envio.Origen.IdUbicacion,
                        Direccion = factura.Envio.Origen.Direccion,
                        Localidad = new LocalidadDto
                        {
                            IdLocalidad = factura.Envio.Origen.Localidad.IdLocalidad,
                            Nombre = factura.Envio.Origen.Localidad.Nombre,
                            Provincia = new ProvinciaDto
                            {
                                IdProvincia = factura.Envio.Origen.Localidad.Provincia.IdProvincia,
                                Nombre = factura.Envio.Origen.Localidad.Provincia.Nombre,
                                Pais = new PaisDto
                                {
                                    IdPais = factura.Envio.Origen.Localidad.Provincia.Pais.IdPais,
                                    Nombre = factura.Envio.Origen.Localidad.Provincia.Pais.Nombre
                                }
                            }
                        }
                    },
                    Destino = new UbicacionDto
                    {
                        IdUbicacion = factura.Envio.Destino.IdUbicacion,
                        Direccion = factura.Envio.Destino.Direccion,
                        Localidad = new LocalidadDto
                        {
                            IdLocalidad = factura.Envio.Destino.Localidad.IdLocalidad,
                            Nombre = factura.Envio.Destino.Localidad.Nombre,
                            Provincia = new ProvinciaDto
                            {
                                IdProvincia = factura.Envio.Destino.Localidad.Provincia.IdProvincia,
                                Nombre = factura.Envio.Destino.Localidad.Provincia.Nombre,
                                Pais = new PaisDto
                                {
                                    IdPais = factura.Envio.Destino.Localidad.Provincia.Pais.IdPais,
                                    Nombre = factura.Envio.Destino.Localidad.Provincia.Pais.Nombre
                                }
                            }
                        }
                    },
                    FechaEntregaReal = factura.Envio.FechaEntregaReal,
                },
                Cliente = new ClienteDto
                {
                    IdCliente = factura.Cliente.IdCliente,
                    Nombre = factura.Cliente.Nombre,
                    Telefono = factura.Cliente.Telefono,
                    Email = factura.Cliente.Email
                },
                FechaEmision = factura.FechaEmision,
                Estado = factura.Estado,
                Subtotal = factura.Subtotal,
                Iva = factura.Iva,
                Total = factura.Total
            };
        }

        // update factura
        public async Task<FacturaDto?> UpdateFacturaAsync(int id, UpdateFacturaDto facturaDto)
        {
            if (facturaDto == null)
            {
                throw new ArgumentNullException(nameof(facturaDto), "La factura no puede ser nula.");
            }

            var factura = await _context.Facturas.FindAsync(id);
            if (factura == null) return null;

            factura.NumeroFactura = facturaDto.NumeroFactura;
            factura.IdEnvio = facturaDto.IdEnvio;
            factura.IdCliente = facturaDto.IdCliente;
            factura.FechaEmision = facturaDto.FechaEmision;
            factura.FechaVencimiento = facturaDto.FechaVencimiento;
            factura.Subtotal = facturaDto.Subtotal;
            factura.Iva = facturaDto.Iva;
            factura.Total = facturaDto.Total;
            factura.Estado = facturaDto.Estado;

            await _context.SaveChangesAsync();

            return new FacturaDto
            {
                IdFactura = factura.IdFactura,
                NumeroFactura = factura.NumeroFactura,
                Envio = new EnvioDto
                {
                    IdEnvio = factura.IdEnvio,
                    Origen = new UbicacionDto
                    {
                        IdUbicacion = factura.Envio.Origen.IdUbicacion,
                        Direccion = factura.Envio.Origen.Direccion,
                        Localidad = new LocalidadDto
                        {
                            IdLocalidad = factura.Envio.Origen.Localidad.IdLocalidad,
                            Nombre = factura.Envio.Origen.Localidad.Nombre,
                            Provincia = new ProvinciaDto
                            {
                                IdProvincia = factura.Envio.Origen.Localidad.Provincia.IdProvincia,
                                Nombre = factura.Envio.Origen.Localidad.Provincia.Nombre,
                                Pais = new PaisDto
                                {
                                    IdPais = factura.Envio.Origen.Localidad.Provincia.Pais.IdPais,
                                    Nombre = factura.Envio.Origen.Localidad.Provincia.Pais.Nombre
                                }
                            }
                        }
                    },
                    Destino = new UbicacionDto
                    {
                        IdUbicacion = factura.Envio.Destino.IdUbicacion,
                        Direccion = factura.Envio.Destino.Direccion,
                        Localidad = new LocalidadDto
                        {
                            IdLocalidad = factura.Envio.Destino.Localidad.IdLocalidad,
                            Nombre = factura.Envio.Destino.Localidad.Nombre,
                            Provincia = new ProvinciaDto
                            {
                                IdProvincia = factura.Envio.Destino.Localidad.Provincia.IdProvincia,
                                Nombre = factura.Envio.Destino.Localidad.Provincia.Nombre,
                                Pais = new PaisDto
                                {
                                    IdPais = factura.Envio.Destino.Localidad.Provincia.Pais.IdPais,
                                    Nombre = factura.Envio.Destino.Localidad.Provincia.Pais.Nombre
                                }
                            }
                        }
                    },
                    FechaEntregaReal = factura.Envio.FechaEntregaReal,
                },
                Cliente = new ClienteDto
                {
                    IdCliente = factura.Cliente.IdCliente,
                    Nombre = factura.Cliente.Nombre,
                    Telefono = factura.Cliente.Telefono,
                    Email = factura.Cliente.Email
                },
                FechaEmision = factura.FechaEmision,
                Estado = factura.Estado,
                Subtotal = factura.Subtotal,
                Iva = factura.Iva,
                Total = factura.Total
            };
        }


        public async Task<bool> DeleteFacturaAsync(int id)
        {
            var factura = await _context.Facturas.FindAsync(id);

            if (factura == null) return false;

            factura.Deleted = true;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<FacturaDto>> GetFacturasPorEstadoAsync(EstadoFactura estado)
        {
            return await _context.Facturas
                .Where(f => f.Estado == estado)
                .Select(f => new FacturaDto
                {

                }).ToListAsync();
        }

        public async Task<bool> ActualizarEstadoFacturaAsync(int facturaId, EstadoFactura nuevoEstado)
        {
            var factura = await _context.Facturas.FindAsync(facturaId);

            if (factura == null) return false;

            factura.Estado = nuevoEstado;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
