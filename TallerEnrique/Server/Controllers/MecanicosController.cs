﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TallerEnrique.Server.Data;
using TallerEnrique.Shared.Entidades;

namespace TallerEnrique.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MecanicosController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        public MecanicosController(ApplicationDbContext context)
        {
            this.context = context;
        }
        [HttpPost]
        public async Task<ActionResult<int>> Post(Mecanico mecanico)
        {
            context.Add(mecanico);
            await context.SaveChangesAsync();
            return mecanico.Id;
        }

        [HttpGet]
        public async Task<ActionResult<List<Mecanico>>> Get()
        {
            return await context.Mecanicos.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Mecanico>> Get(int id)
        {
            return await context.Mecanicos.FirstOrDefaultAsync(x => x.Id == id);
        }

        [HttpPut]
        public async Task<ActionResult> Put(Mecanico categoria)
        {
            context.Attach(categoria).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await context.Mecanicos.AnyAsync(x => x.Id == id);
            if (!existe) { return NotFound(); }
            context.Remove(new Mecanico { Id = id });
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
