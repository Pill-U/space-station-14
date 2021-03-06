using Content.Server.GameObjects.Components.Items;
using Content.Shared.GameObjects.EntitySystems;
using Robust.Shared.GameObjects;

namespace Content.Server.GameObjects.Components.Explosion
{
    [RegisterComponent]
    public class ExplosionLaunchedComponent : Component, IExAct
    {
        public override string Name => "ExplosionLaunched";

        void IExAct.OnExplosion(ExplosionEventArgs eventArgs)
        {
            if (Owner.Deleted)
                return;

            var sourceLocation = eventArgs.Source;
            var targetLocation = eventArgs.Target.Transform.Coordinates;
            var direction = (targetLocation.ToMapPos(Owner.EntityManager) - sourceLocation.ToMapPos(Owner.EntityManager)).Normalized;

            var throwForce = eventArgs.Severity switch
            {
                ExplosionSeverity.Heavy => 30,
                ExplosionSeverity.Light => 20,
                _ => 0,
            };
            Owner.TryThrow(direction * throwForce);
        }
    }
}
