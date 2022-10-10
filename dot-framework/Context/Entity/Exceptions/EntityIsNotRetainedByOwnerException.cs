namespace Dot.Framework
{
    public class EntityIsNotRetainedByOwnerException : FWException
    {

        public EntityIsNotRetainedByOwnerException(IEntity entity, object owner)
            : base("'" + owner + "' cannot release " + entity + "!\n" +
                   "Entity is not retained by this object!",
                "An entity can only be released from objects that retain it.")
        {
        }
    }
}
