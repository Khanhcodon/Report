INSERT into docfield_doctype_workflow( workflowid , doctypeid, isactivated)
select workflowid , doctypeid, isactivated from workflow