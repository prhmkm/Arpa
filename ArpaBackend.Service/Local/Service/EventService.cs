using ArpaBackend.Core.Model.Base;
using ArpaBackend.Data.Base;
using ArpaBackend.Domain.DTOs;
using ArpaBackend.Domain.Models;
using ArpaBackend.Service.Local.Interface;
using Microsoft.Extensions.Options;

namespace ArpaBackend.Service.Local.Service
{
    public class EventService : IEventService
    {
        IRepositoryWrapper _repository;
        private readonly AppSettings _appSettings;

        public EventService(IRepositoryWrapper repository, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _repository = repository;
        }

        public void AddEvent(Event _event)
        {
            _repository.Event.AddEvent(_event);
        }

        public List<EventDTO.ShowEvents> GetAllEvents()
        {
            return _repository.Event.GetAllEvents();
        }

        public List<EventDTO.ShowEvents> GetAllEvents(int pagesize)
        {
            return _repository.Event.GetAllEvents(pagesize);
        }

        public List<EventDTO.BOShowEvents> BOGetAllEvents()
        {
            return _repository.Event.BOGetAllEvents();
        }

        public List<EventDTO.BOShowEvents> BOGetAllEvents(int pagesize)
        {
            return _repository.Event.BOGetAllEvents(pagesize);
        }

        public void UpdateEvent(Event _event)
        {
            _repository.Event.UpdateEvent(_event);
        }

        public EventDTO.ShowEvents GetEventById(int Id)
        {
            return _repository.Event.GetEventById(Id);
        }

        public Event GetEvent(int Id)
        {
            return _repository.Event.GetEvent(Id);
        }

        public EventDTO.BOShowEventsImages ShowEventImages(int Id)
        {
            return _repository.Event.ShowEventImages(Id);
        }
    }
}
