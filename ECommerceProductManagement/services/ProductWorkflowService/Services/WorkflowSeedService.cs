using ProductWorkflowService.Data;

namespace ProductWorkflowService.Services;

public static class WorkflowSeedService
{
    public static void Seed(WorkflowDbContext context)
    {
        context.Database.EnsureCreated();
    }
}
