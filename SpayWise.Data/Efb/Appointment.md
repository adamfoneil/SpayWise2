Appointment : BaseTable
#LocationId
#TypeId
#PatientId
#Date DateOnly
DropOff TimeOnly
Pickup DateTime
StatusId AppointmentStatus
VolumeClientId Client?
CopayClientId Client?
TransportClientId Client?
HasRabiesVaccineProof bool
KennelSizeId?
Points int
PointsClientId Client? // if null points go to clinic


